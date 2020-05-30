import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, InputGroup, FormControl } from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";

class ChatRoom extends Component {
  constructor() {
    super();
      this.state = {
          userSelf: [{
              name: 'You',
              messages: [
                  { text: 'Ricky, just go to the grocery store.........' },
                  { text: 'Ricky, just go to the grocery store.........' }
              ]
          }]      
    };
  }



  userOneIcon = () => {
    return (
      <Container className="user-icon">
        <Circle
          className="d-inline-block"
          color="white"
          width="12"
          height="12"
        />
        <h6 className="d-inline">You</h6>
      </Container>
    );
  };

  userTwoIcon = () => {
    return (
      <Container className="user-icon">
        <Circle
          className="d-inline-block"
          color="white"
          width="12"
          height="12"
        />
        <h6 className="d-inline">Ricky Bobby</h6>
      </Container>
    );
  };

    render() {

        console.log()

      const userSelfMessages = this.state.userSelf.map((user, index) => {
          return (
        <div key={index}>
        <br />
        <br />
        <hr />
        <h6>{user.name}:</h6>
        <p>{user.messages[index].text}</p>
            </div>
          )
    });

    return (
      <div className="container">
        <div className="row">
          <div className="col" id="one">
            <h5>Users</h5>
          </div>
          <div className="col" id="two">
            {userSelfMessages}
          </div>
          <div className="col" id="three">
            <br />
            {this.userOneIcon()}
            {this.userTwoIcon()}
          </div>
          <div className="col" id="four">
            <InputGroup className="mb-3">
              <FormControl
                placeholder="Message"
                aria-label="Recipient's message"
                aria-describedby="basic-addon2"
              />
              <InputGroup.Append>
                <Button className="submit-button" variant="outline-secondary">
                  Send
                </Button>
              </InputGroup.Append>
            </InputGroup>
          </div>
        </div>
      </div>
    );
  }
}

export default ChatRoom;
