import React, { Component, useState } from "react";
import { Route, NavLink, Routes, HashRouter } from "react-router-dom";

import Login from "./Login";
import Profile from "./Profile";
import Documents from "./Documents";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isAuthenticated: false
    };
  }

  render() {

  return (
    <HashRouter>
      <div className="App">
        <nav class="navBar text">
          <li class="navLink"><NavLink to="/profile">Профиль</NavLink></li>
          <li class="navLink"><NavLink to="/documents">Документы</NavLink></li>
        </nav>
        <div className="content">
          <Routes>
            <Route exact path="/profile" element={this.state.isAuthenticated ? <Profile /> : <Login />}></Route>
            <Route exact path="/documents" element={this.state.isAuthenticated ? <Documents /> : <Login />}></Route>
          </Routes>
        </div>
      </div>
    </HashRouter>
  );
}
}
export default App;