import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, Row, Col, Form } from "react-bootstrap";
import "./index.css";
import axios from "axios";

class LandingPage extends Component {
  constructor() {
    super();
    this.state = {
      newUsername: "",
      newPassword: "",
      existingUsername: "",
      existingPassword: "",
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

  handleNewUserSubmit = e => {
    e.preventDefault();
    this.postNewUser();
  };

  handleLogInSubmit = e => {
    e.preventDefault();
    this.postNewUser();
  };

  postNewUser = () => {
    axios
      .post("/api/register", {
        username: this.state.newUsername,
        password: this.state.newPassword
      })
      .then(res => {
        localStorage.setItem("session_id", res.data[0].id);
      })
      .catch(err => {
        if (err.response.data.title === "empty username") {
          this.setState({
            errorMessage: "Please choose a username."
          });
        }
        if (err.response.data.title === "empty password") {
          this.setState({
            errorMessage: "Please choose a password."
          });
        }
        if (err.response.data.title === "redundant username") {
          this.setState({
            errorMessage:
              "The username you chose is already taken.  Please try another entry."
          });
        }
      });
  };

  logInUser = () => {
    axios
      .post("/api/authorize", {
        username: this.state.existingUsername,
        password: this.state.existingPassword
      })
      .then(res => {
        localStorage.setItem("session_id", res.data[0].id);
      })
      .catch(err => {
        if (err.response.data.title === "empty username") {
          this.setState({
            errorMessage: "Please choose a username."
          });
        }
        if (err.response.data.title === "empty password") {
          this.setState({
            errorMessage: "Please choose a password."
          });
        }
        if (err.response.data.title === "redundant username") {
          this.setState({
            errorMessage:
              "The username you chose is already taken.  Please try another entry."
          });
        }
      });
  };

  render() {
    console.log(this.state.errorMessage);
    return (
      <div>
        <Container className="top-container" fluid>
          <Row className="h-100">
            <Col>
              <h1 className="gab-logo">
                <span className="purple-gab">The Gab</span> Chat Room
              </h1>
            </Col>
            <Col>
              <Form className="sign-in-form" onSubmit={this.handleLogInSubmit}>
                <Row className="sign-in-row">
                  <Col className="col-lg-4 username-col">
                    <Form.Label className="top-form-label">Username</Form.Label>
                    <Form.Control
                      type="input"
                      className="input-sign-in"
                      onChange={this.handleChange}
                      name="existingUsername"
                    />
                  </Col>
                  <Col className="col-lg-4 password-col">
                    <Form.Label className="top-form-label">Password</Form.Label>
                    <Form.Control
                      type="input"
                      className="input-sign-in"
                      onChange={this.handleChange}
                      name="existingPassword"
                    />
                  </Col>
                </Row>
              </Form>
            </Col>
          </Row>
        </Container>
        <Container className="bottom-container" fluid>
          <Row className="w-100">
            <Col className="w-100">
              <h1 class="join-text">Join The Gab Community</h1>
              <h2 class="painless-text">It's quick and painless.</h2>
            </Col>
          </Row>
          <br />
          <Row>
            <Col>
              <Form
                className="mt-3 sign-up-form"
                onSubmit={this.handleNewUserSubmit}
              >
                <Form.Group controlId="formBasicEmail">
                  <Form.Label>Choose username</Form.Label>
                  <Form.Control
                    type="input"
                    placeholder="Enter a new username"
                    name="newUsername"
                    onChange={this.handleChange}
                  />
                </Form.Group>

                <Form.Group controlId="formBasicPassword">
                  <Form.Label>Choose password</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder="Enter a new password"
                    name="newPassword"
                    onChange={this.handleChange}
                  />
                </Form.Group>
                <Button variant="light" type="submit">
                  Submit
                </Button>
              </Form>
              <br />
              <h2 className="text-center error-text">
                {this.state.errorMessage}
              </h2>
            </Col>
          </Row>
          <div className="container-fluid footer-container">
            <footer>
              <h6 className="text-center footer-text mt-2">
                Created by Reid Muchow
              </h6>
            </footer>
          </div>
        </Container>
      </div>
    );
  }
}

export default LandingPage;
