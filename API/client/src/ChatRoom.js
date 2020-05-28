import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Form } from 'react-bootstrap';
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
        <div class="row">
          <div class="col" id="one">
            <h2>Users</h2>
          </div>
                    <div class="col" id="two">
                        <br />
                        <br />
                        <h6>Ricky Bobby:</h6>
                        <p>Can anyone on here please tell me where I can find some shake n' bake??!!!</p>
                        <br />
                        <br />
                        <hr />             
                        <h6>You:</h6>
                        <p>Ricky, just got to the grocery store.........</p>
                        <br />
                        <br />
                        <hr />
                        <h6>Ricky:</h6>
                        <p>Ten, four!</p>
                        <br />
                        <br />
                        <hr />
                        <h6>Ricky:</h6>
                        <p>Can you give me a ride to the grocery store?</p>
                        <br />
                        <br />
                        <hr />
                        <h6>You:</h6>
                        <p>Why don't you drive yourself?</p>
                        <br />
                        <br />
                        <hr />
                        <h6>Ricky:</h6>
                        <p>I only drive my race car Reid...</p>
                        <br />
                        <br />
                        <hr />
                        <h6>You:</h6>
                        <p>That's too bad Ricky, you're a prima donna.</p>
                        <br />
                        <br />
                        <hr />
          </div> 
                    <div class="col" id="three">
                        <br />                       
                        <h6>You</h6>
                        <h6>Ricky Bobby</h6>
          </div>
          <div class="col" id="four">
            <Form.Group>
                            <Form.Control type="text" placeholder="Message" />
                            </Form.Group>
          </div>
        </div>
      </div>
            );
    }

}

export default ChatRoom;


