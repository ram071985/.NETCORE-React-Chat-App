import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, InputGroup, FormControl } from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";

class ChatRoom extends Component {
  constructor() {
    super();
    this.state = {};
  }

  UserOneIcon = () => {
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

  UserTwoIcon = () => {
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
    return (
      <div className="container">
        <div className="row">
          <div className="col" id="one">
            <h5>Users</h5>
          </div>
          <div className="col" id="two">
            <br />
            <br />
            <h6>Ricky Bobby:</h6>
            <p>
              Can anyone on here please tell me where I can find some shake n'
              bake??!!!
            </p>
            <br />
            <br />
            <hr />
            <h6>You:</h6>
            <p>Ricky, just go to the grocery store.........</p>
            <br />
            <br />
            <hr />
            <h6>Ricky:</h6>
            <p>Ten, four!</p>
            <br />
            <br />
            <hr />
            <h6>Ricky:</h6>
            <p>Can you give me a ride to the grocery store?</p>
            <br />
            <br />
            <hr />
            <h6>You:</h6>
            <p>Why don't you drive yourself?</p>
            <br />
            <br />
            <hr />
            <h6>Ricky:</h6>
            <p>I only drive my race car Reid...</p>
            <br />
            <br />
            <hr />
            <h6>You:</h6>
            <p>That's too bad Ricky, you're a prima donna.</p>
            <br />
            <br />
            <hr />
          </div>
          <div className="col" id="three">
            <br />
            {this.UserOneIcon()}
            {this.UserTwoIcon()}
          </div>
          <div className="col" id="four">
                    <InputGroup className="mb-3">
                        <FormControl
                            placeholder="Message"
                            aria-label="Recipient's message"
                            aria-describedby="basic-addon2"
                        />
                        <InputGroup.Append>
                            <Button className="submit-button" variant="outline-secondary">Submit</Button>
                        </InputGroup.Append>
                    </InputGroup>
          </div>
        </div>
      </div>
    );
  }
}

export default ChatRoom;
