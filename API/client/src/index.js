import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import * as serviceWorker from "./serviceWorker";
import ProtectedRoute from "./ProtectedRoute";
import LandingPage from "./LandingPage";
import ChatRoom from "./ChatRoom";

ReactDOM.render(
  <BrowserRouter>
    <Switch>
      <Route path="/login" component={LandingPage} />
      <ProtectedRoute exact={true} path="/" component={ChatRoom} />
      <ProtectedRoute component={ChatRoom} />
    </Switch>
  </BrowserRouter>,
  document.getElementById("root")
);

serviceWorker.unregister();
