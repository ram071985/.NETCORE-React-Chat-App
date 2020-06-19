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
import NewUserForm from "./NewUserForm";
import axios from "axios";

class LandingPage extends Component {
  constructor() {
    super();
    this.state = {
      sessionId: []
    };
  }

  handleChange = event => {
    const { name, value } = event.target;
    this.setState({
      [name]: value
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    this.postNewUser();
  };

  handleUserLogIn = () => {};

  postNewUser = () => {
      axios.post("/api/authpractice",{
        data: {
        userName: "ried",
        password: "ried"
        }
      })
      .then( res => {
        console.log(res);
    });
  };

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
              <Card.Link onClick={this.handleClick}>
                <h6 className="prompt-links">Create New Account</h6>
              </Card.Link>
              <Card.Link href="#">
                <h6 className="prompt-links">Log In</h6>
              </Card.Link>
            </Card.Body>
          </Card>
        </div>
        <div>
          <div className="container">
            <Form className="mt-3">
              <Form.Group controlId="formBasicEmail">
                <Form.Label>Choose username</Form.Label>
                <Form.Control
                  type="input"
                  placeholder="Enter a new username"
                  name="username"
                  onChange={this.handleChange}
                />
                <Form.Text className="text-muted">
                  We'll never share your email with anyone else.
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="formBasicPassword">
                <Form.Label>Choose password</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="Enter a new password"
                  name="password"
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Button
                variant="primary"
                type="submit"
                onSubmit={this.handleSubmit}
              >
                Submit
              </Button>
            </Form>
          </div>
        </div>
      </div>
    );
  }
}

export default LandingPage;
