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
           <div class="container">
  <div class="row justify-content-start">
    <div class="col-4" id="one">
      <h1>Users</h1>
    </div>
    <div class="col-4" id="two">
      One of two columns
    </div>
  </div>
  <div class="row justify-content-start">
   <div class="col-4" id="three">

      One of three columns
    </div>
   <div class="col-4" id="four">
      One of four columns
    </div>
                </div>
                </div>
            );
    }

}

export default ChatRoom;


