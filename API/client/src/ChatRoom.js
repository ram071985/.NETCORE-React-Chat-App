import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, InputGroup, FormControl } from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";
import { Redirect } from "react-router-dom";
import axios from "axios";

class ChatRoom extends Component {
  constructor() {
    super();
    this.state = {
      users: [],
      messages: [],
      messageInput: "",
      isLoggedIn: true
    };
  }

  componentDidMount() {
    this.getMessagesFromDatabase();
    this.getUsers();
  }

  getMessagesFromDatabase = () => {
    axios.get("/api/messages", {}).then(res => {
      console.log(res.data);
      this.setState({
        messages: res.data
      });
    });
  };

  getUsers = () => {
    let id = localStorage.getItem("user_id");
    axios.get(`/api/users/${id}`, {}).then(res => {
      console.log(res.data);
      this.setState({
        users: res.data
      });
    });
  };

  handleChange = event => {
    const { name, value } = event.target;
    this.setState({
      [name]: value
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    this.clearInput();
  };

  handleClick = () => {
    localStorage.removeItem("session_id");
    this.setState({
      isLoggedIn: false
    });
  };

  addMessage = () => {
    const addNewMessage = {
      id: Math.random(),
      username: "You",
      text: this.state.messageInput,
      created_date: new Date()
    };
    this.setState(prevState => {
      return {
        messages: [...prevState.messages, addNewMessage]
      };
    });
  };

  clearInput = () => {
    this.setState({
      messageInput: ""
    });
  };

  render() {
    console.log(this.state.messages);
    if (this.state.isLoggedIn === false) {
      return <Redirect to="/login" />;
    }

    const userMessages = this.state.messages.map(message => {
      return (
        <div key={message.id}>
          <h6>{message.username}:</h6>
          <p>{message.text}</p>
          <br />
          <br />
          <hr />
        </div>
      );
    });

    return (
      <div className="container">
        <div className="row">
          <div className="col" id="one">
            <h5 className="users-header">Users</h5>
          </div>
          <div className="col" id="two">
            {userMessages}
          </div>
          <div className="col" id="three">
            <br />
            <Container className="user-icon">
              <Circle
                className="d-inline-block"
                color="white"
                width="12"
                height="12"
              />
              <h6 className="d-inline">{this.state.users.username}</h6>
            </Container>
          </div>
          <div className="col" id="five">
            <Button
              onClick={this.handleClick}
              className="log-out-button"
              variant="secondary"
              size="sm"
            >
              Log Out
            </Button>
          </div>
          <div className="col" id="four">
            <form onSubmit={this.handleSubmit}>
              <InputGroup className="mb-3">
                <FormControl
                  placeholder="Message"
                  aria-label="Recipient's message"
                  aria-describedby="basic-addon2"
                  name="messageInput"
                  value={this.state.messageInput}
                  type="input"
                  onChange={this.handleChange}
                />
                <InputGroup.Append>
                  <Button
                    className="submit-button"
                    variant="outline-secondary"
                    type="submit"
                  >
                    Send
                  </Button>
                </InputGroup.Append>
              </InputGroup>
            </form>
          </div>
        </div>
      </div>
    );
  }
}

export default ChatRoom;
