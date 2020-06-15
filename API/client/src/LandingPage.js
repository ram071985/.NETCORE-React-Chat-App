import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, InputGroup, FormControl } from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";



class LandingPage extends Component {
    constructor() {
      super();
      this.state = {
        users: [
          { id: 1, username: "You" },
          { id: 2, username: "Fred" }
        ],
        messages: [
          {
            id: 1,
            username: "You",
            text: "Hello world!",
            created_date: new Date()
          },
          {
            id: 2,
            username: "Fred",
            text: "Hello back at ya",
            created_date: new Date()
          },
          {
            id: 3,
            username: "Fred",
            text: "The World is beautiful.",
            created_date: new Date()
          }
        ],
        messageInput: ""
      };
    }

    componentDidMount() {
        
    }
  
    handleChange = event => {
      const { name, value } = event.target;
      this.setState({
        [name]: value
      });
    };
  
    handleSubmit = (e) => {
      e.preventDefault();
      this.addMessage();
      this.clearInput();
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
      const usersLoggedIn = this.state.users.map(user => {
        return (
          <Container key={user.id} className="user-icon">
            <Circle
              className="d-inline-block"
              color="white"
              width="12"
              height="12"
            />
            <h6 className="d-inline">{user.username}</h6>
          </Container>
        );
      });
  
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
              <h5>Users</h5>
            </div>
            <div className="col" id="two">
              {userMessages}
            </div>
            <div className="col" id="three">
              <br />
              {usersLoggedIn}
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
  
  export default LandingPage;