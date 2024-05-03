import React from "react";
import { Route, Routes, HashRouter } from "react-router-dom";

import Login from "./Login";
import Profile from "./Profile";
import Documents from "./Documents";
import Register from "./Register";

window.server = "https://localhost:7273";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isAuthenticated: false
    };
  }

  refresh = async (func) => {
    fetch(window.server + "/api/users/refresh", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('refreshToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.setState({ redirectToLogin: true });
        return;
      }
      if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (data.statusCode) {
        alert(data.message);
      }
      else{
        localStorage.setItem("accessToken", data.accessToken);
        localStorage.setItem("refreshToken", data.refreshToken);
  
        this.setAuthenticated(true);

        func();
      }
    });
  };

  setAuthenticated = (isAuthenticated) => {
    this.setState({ isAuthenticated });
  }

  render() {

  return (
    <HashRouter>
      <div className="App">
        <nav class="navBar text">
          {this.state.isAuthenticated ? <a class="navLink text" href="#/profile">Профиль</a> : null}
          {this.state.isAuthenticated ? <a class="navLink text" href="#/documents">Документы</a> : null }
        </nav>
        <div className="content">
          <Routes>
            <Route exact path="/" element={this.state.isAuthenticated ? <Profile refresh={this.refresh} /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/profile" element={this.state.isAuthenticated ? <Profile refresh={this.refresh} /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/documents" element={this.state.isAuthenticated ? <Documents /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/register" element=<Register setAuthenticated={this.setAuthenticated}/>></Route>
            <Route exact path="/login" element=<Login setAuthenticated={this.setAuthenticated}/>></Route>
          </Routes>
        </div>
      </div>
    </HashRouter>
);
}
}
export default App;