import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  Container,
  Button,
  Card,
  InputGroup,
  FormControl,
  Form,
  Col,
  Row
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
        <div className="card-container">
          <Card style={{ width: "18rem" }} className="log-in-card">
            <Card.Body>
              <Card.Title>
                <h5 className="prompt-title">
                  Welcome To <span className="gab-color">The Gab</span> Chat
                  Room
                </h5>
              </Card.Title>
              <Card.Link href="#">
                <h6 className="prompt-links">Create New Account</h6>
              </Card.Link>
              <Card.Link href="#">
                <h6 className="prompt-links">Log In</h6>
              </Card.Link>
              <Form className="mt-3">
                <Form.Group controlId="formBasicEmail">
                  <Form.Label>Choose username</Form.Label>
                  <Form.Control type="email" placeholder="Enter a new username" />
                  <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                  </Form.Text>
                </Form.Group>

                <Form.Group controlId="formBasicPassword">
                  <Form.Label>Choose password</Form.Label>
                  <Form.Control type="password" placeholder="Enter a new password" />
                </Form.Group>
                <Button variant="primary" type="submit">
                  Submit
                </Button>
              </Form>
            </Card.Body>
          </Card>
        </div>
      </div>
    );
  }
}

export default LandingPage;
