import React, { Component } from "react";
import {
  BrowserRouter as Router,
  Redirect
} from "react-router-dom";

class Authenticate extends Component {

    componentDidMount() {
           return(
            <Router>
            <Redirect to={{
              pathname: "/thegabchat"
            }} />      
          </Router>
           )
    }
  render() {
    const sessionId = localStorage.getItem("session_id");
    return (

    );
  }
}

export default Authenticate;