import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button, Card, Form } from "react-bootstrap";
import "./index.css";
import axios from "axios";

class LandingPage extends Component {
  constructor() {
    super();
    this.state = {
      username: "",
      password: "",
      errorMessage: ""
    };
  }

  handleChange = event => {
    const { name, value } = event.target;
    this.setState({
      [name]: value,
      errorMessage: ""
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    this.postNewUser();
  };

  handleUserLogIn = () => {};

  postNewUser = () => {
    axios
      .post("/api/register", {
        username: this.state.username,
        password: this.state.password
      })
      .then(res => {
        localStorage.setItem("session_id", res.data[0].id);
        console.log(res.data[0].id);
      })
      .catch(err => {
        if (err.response.status === 500) {
          this.setState({
            errorMessage: "Please enter a username and password."
            
          });
        }
        console.log(this.state.errorMessage)
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
            <Form className="mt-3" onSubmit={this.handleSubmit}>
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
              <Button variant="primary" type="submit">
                Submit
              </Button>
              {this.state.errorMessage}
            </Form>
          </div>
        </div>
      </div>
    );
  }
}

export default LandingPage;
