import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, Row, Col, Form } from "react-bootstrap";
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
        console.log(res.data);
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
    return (
      <div>
        <Container className="top-container" fluid>
          <Row className="h-100">
            <Col>
              <h1 className="gab-logo">
                <span className="purple-gab">The Gab</span> Chat Room
              </h1>
            </Col>
            <Col className="sign-in-form">
              <Form>
                <Row>
                  <Col className="col-lg-4 username-col">
                    <Form.Control
                      className="username-sign-in"
                      placeholder="First name"
                    />
                  </Col>
                  <Col className="col-lg-4 password-col">
                    <Form.Control
                      className="password-sign-in"
                      placeholder="Last name"
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
          <Row className="w-50">
            <Col>
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
            </Col>
          </Row>
        </Container>
      </div>
    );
  }
}

export default LandingPage;
