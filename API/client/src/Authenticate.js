import React, { Component } from "react";
import {
  BrowserRouter as Router,
  Route,
  Switch,
  Redirect
} from "react-router-dom";
import LandingPage from "./LandingPage";
import ChatRoom from "./ChatRoom";

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
      <Router>
        <Switch>
          <Route exact path="/">
            {(sessionId !== null) ? <ChatRoom /> : <LandingPage />}
          </Route>
          <Route path="/thegabchat">
             <ChatRoom />
          </Route>
        </Switch>
      </Router>
    );
  }
}

export default Authenticate;