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
      isLoggedIn: true,
      sessionId: 0
    };
  }

  componentDidMount() {
    let id = localStorage.getItem("session_id");
    this.getMessagesFromDatabase();
    this.getUsers();
    this.setState({
      sessionId: id
    });
  }

  getMessagesFromDatabase = () => {
    axios.get("/api/messages", {}).then(res => {
      const messageDates = res.data.sort((a, b) => {
        const message1 = new Date(b.createdDate);
        const message2 = new Date(a.createdDate);
        return message1 - message2;
      });
      this.setState({
        messages: messageDates
      });
    });
  };

  getUsers = () => {
    let parseId = parseInt(localStorage.getItem("user_id"));
    axios.get(`/api/users/${parseId}`, {}).then(res => {
      this.setState({
        users: res.data
      });
    });
  };

  putNewUserName = () => {
    let parseId = parseInt(localStorage.getItem("user_id"));
    axios
      .put("/api/users", {
        userId: parseId,
        newUsername: this.state.newUsername
      })
      .then(res => {
        console.log(res);
        this.getUsers();
      });
  };
  postNewMessage = () => {
    let parseId = parseInt(localStorage.getItem("session_id"));
    axios
      .post("/api/messages", {
        sessionId: parseId,
        text: this.state.messageInput
      })
      .then(res => {
        const messageDates = res.data.sort((a, b) => {
          const message1 = new Date(b.createdDate);
          const message2 = new Date(a.createdDate);
          return message1 - message2;
        });
        this.setState({
          messages: messageDates
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
    this.postNewMessage();
    this.getMessagesFromDatabase();
    this.clearInput();
  };

  handleClick = () => {
    localStorage.removeItem("session_id");
    localStorage.removeItem("user_id");
    this.setState({
      isLoggedIn: false
    });
  };

  onKeyPress = e => {
    if (e.which === 13) {
      e.preventDefault();
      this.postNewMessage();
      this.getMessagesFromDatabase();
      this.clearInput();
    }
  };

  clearInput = () => {
    this.setState({
      messageInput: ""
    });
  };

  render() {
    let id = localStorage.getItem("user_id");
    console.log(id);

    if (this.state.isLoggedIn === false) {
      return <Redirect to="/login" />;
    }

    const userMessages = this.state.messages.map(message => {
      return (
        <div
          className="card message-card"
          style={{ width: "40rem" }}
          key={message.createdDate}
        >
          <div className="card-body">
            <h6 className="card-title">{message.username}</h6>
            <p className="card-text">{message.text}</p>
          </div>
        </div>
      );
    });
    return (
      <div className="container">
        <div className="row h-100">
          <div className="col-xs h-100 w-25 users-col">
            <h5 className="d-inline-block users-header">Users</h5>
            <a
              href=""
              onClick={this.handleClick}
              className="d-inline-block justify-content-right log-out-button"
              variant="secondary"
              size="sm"
            >
              Log Out
            </a>
            <hr className="users-border" />
            <br />
            {this.sortedDates}
            <Circle
              className="d-inline-block ml-3 mb-1"
              color="white"
              width="12"
              height="12"
            />
            <h6 className="d-inline ml-2">{this.state.users.username}</h6>
          </div>
          <div className="h-100 messages-col mt-5">{userMessages}</div>
          <form onSubmit={this.handleSubmit} onKeyPress={this.onKeyPress}>
            <InputGroup className="message-input-group">
              <FormControl
                className="message-input"
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
    );
  }
}

export default ChatRoom;
