import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  Container,
  Button,
  Card,
  InputGroup,
  FormControl
} from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";

class LandingPage extends Component {
  constructor() {
    super();
    this.state = {};
  }

  render() {
    return (
      <div>
        <div className="container launch-container">
          <Button
            variant="primary"
            size="lg"
            className="mb-4 launch-button"
          >
            Launch Chat
          </Button>{" "}
          <br />
        </div>
        <div className="card-container">
        <Card style={{ width: "18rem" }} className="log-in-card">
            <Card.Body>
              <Card.Title><h5 className="prompt-title">Welcome To The Gab Chat Room</h5></Card.Title>
              <Card.Link href="#"><h6 className="prompt-links">Create New Account</h6></Card.Link>
              <Card.Link href="#"><h6 className="prompt-links">Log In</h6></Card.Link>
            </Card.Body>
          </Card>
        </div>
      </div>
    );
  }
}

export default LandingPage;
