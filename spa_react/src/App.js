import React from "react";
import { Route, NavLink, Routes, HashRouter } from "react-router-dom";

import Login from "./Login";
import Profile from "./Profile";
import Documents from "./Documents";

window.server = "https://localhost:7273";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isAuthenticated: false
    };
  }

  setAuthenticated = (isAuthenticated) => {
    this.setState({ isAuthenticated });
  }

  render() {

  return (
    <HashRouter>
      <div className="App">
        <nav class="navBar text">
          <a class="navLink text" href="#/profile">Профиль</a>
          <a class="navLink text" href="#/documents">Документы</a>
        </nav>
        <div className="content">
          <Routes>
            <Route exact path="/" element={this.state.isAuthenticated ? <Profile /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/profile" element={this.state.isAuthenticated ? <Profile /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/documents" element={this.state.isAuthenticated ? <Documents /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
          </Routes>
        </div>
      </div>
    </HashRouter>
);
}
}
export default App;