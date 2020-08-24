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
  Row,
} from "react-bootstrap";
import "./index.css";
import { Circle } from "react-feather";

class NewUserForm extends Component {
  render() {
    return (
      <div className="container">
        <Form className="mt-3">
          <Form.Group controlId="formBasicEmail">
            <Form.Label>Choose username</Form.Label>
            <Form.Control
              type="input"
              placeholder="Enter a new username"
              name="username"
              onChange={this.props.handleChange}
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
              onChange={this.props.handleChange}
            />
          </Form.Group>
          <Button variant="primary" type="submit">
            Submit
          </Button>
        </Form>
      </div>
    );
  }
}

export default NewUserForm;
