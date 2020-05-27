import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Row, Col } from 'react-bootstrap';
import "./index.css";



class ChatRoom extends Component {
    constructor() {
        super();
        this.state = {

        };
    }

    render() {
        return (
            <Container className='main-container' fluid>
                <Row>
                    <Col></Col>
                    <Col xs={10}></Col>
                </Row>
            </Container>
            );
    }

}

export default ChatRoom;


