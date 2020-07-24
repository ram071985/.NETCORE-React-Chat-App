import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, Row, Col, Form } from "react-bootstrap";
import "./index.css";
import axios from "axios";
import { Redirect } from "react-router-dom";

class LandingPage extends Component {
  constructor() {
    super();
    this.state = {
      newUsername: "",
      newPassword: "",
      existingUsername: "",
      existingPassword: "",
      errorMessage: "",
      logInErrorMessage: "",
      toChatRoom: false
    };
  }

  handleChange = event => {
    const { name, value } = event.target;
    this.setState({
      [name]: value,
      errorMessage: "",
      logInErrorMessage: ""
    });
  };

  handleNewUserSubmit = e => {
    e.preventDefault();
    this.postNewUser();
  };

  handleLogInSubmit = e => {
    e.preventDefault();
    this.logInUser();

  };

  redirectUser =() => {
    if (this.state.toChatRoom === true) {
      return <Redirect to='/' />
    }
  }

  
  postNewUser = () => {
    axios
      .post("/api/register", {
        username: this.state.newUsername,
        password: this.state.newPassword
      })
        .then(res => {
          localStorage.setItem("session_id", res.data.id);
          localStorage.setItem("user_id", res.data.userId);
     
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
          console.log(res.data);
          localStorage.setItem("session_id", res.data.id);
          localStorage.setItem("user_id", res.data.userId);
        this.setState({
          toChatRoom: true
        })
      })
      .catch(err => {
  
        if (err.response.data.title === "empty username") {
          this.setState({
            logInErrorMessage: "Please enter a username."
          });
        }
        if (err.response.data.title === "empty password") {
          this.setState({
            logInErrorMessage: "Please enter a password."
          });
        }
        if (err.response.data.title === "false username") {
          this.setState({
            logInErrorMessage:
              "The username you chose is already taken.  Please try another entry."
          });
        }
          if (err.response.data.title === "wrong credentials") {
              this.setState({
                  logInErrorMessage: "Username or password are invalid."
              });
          }
      });
  };

    addLastActive = () => {
        let parseId = parseInt(localStorage.getItem("user_id"));
        axios
            .put("/api/users/last_active", {
                userId: parseId
            })
            .then(res => {
                console.log(res);
            });
    }

    render() {
      console.log(this.state.toChatRoom)
    if (this.state.toChatRoom === true) {
      return <Redirect to='/' />
    } 

    return (
      <div className="main-container">
        <Container className="top-container" fluid>
          <Row className="h-100">
            <Col className="gab-column">
              <h1 className="gab-logo">
                <span className="purple-gab">The Gab</span> Chat Room
              </h1>
            </Col>
            <Col>
              <Form className="sign-in-form" onSubmit={this.handleLogInSubmit}>
                <Form.Row className="sign-in-row">
                  <Col className="col-xs-5 username-col">
                    <Form.Label className="top-form-label">Username</Form.Label>
                    <Form.Control
                      type="input"
                      className="input-sign-in"
                      onChange={this.handleChange}
                      name="existingUsername"
                    />
                  </Col>
                  <Col className="col-xs-5 password-col">
                    <Form.Label className="top-form-label">Password</Form.Label>
                    <Form.Control
                      type="input"
                      className="input-sign-in"
                      onChange={this.handleChange}
                      name="existingPassword"
                    />
                  </Col>
              
                    <Button className="login-btn" variant="dark" type="submit">
                    <h6 className="text-center login-text">Log In</h6>
                    </Button>
                    <h6>{this.state.logInErrorMessage}</h6>
            
                </Form.Row>
              </Form>
            </Col>
          </Row>
        </Container>
        <Container className="bottom-container" fluid>
          <Row className="w-100">
            <Col className="w-100">
              <h1 className="join-text">Join The Gab Community</h1>
              <h2 className="painless-text">It's quick and painless.</h2>
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
                Copyright Reid Muchow 2020
              </h6>
            </footer>
          </div>
        </Container>
      </div>
    );
  }
}

export default LandingPage;
