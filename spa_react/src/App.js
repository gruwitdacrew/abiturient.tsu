import React from "react";
import { Route, Routes, HashRouter } from "react-router-dom";

import Login from "./Login";
import Profile from "./Profile";
import Documents from "./Documents";
import Register from "./Register";

window.users = "https://localhost:7273";
window.documents = "https://localhost:7275";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isAuthenticated: localStorage.getItem('refreshToken') !== null
    };
  }

  refresh = async (func) => {
    fetch(window.users + "/api/users/refresh", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('refreshToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        this.setAuthenticated(false);
        return;
      }
      if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data) return;
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
          <button class = "navButt"><a class="navLink text" href="#/">Abiturient.tsu</a></button>
          <span style={{width: "90%"}}></span>
          {this.state.isAuthenticated ? <button class = "navButt"><a class="navLink text" href="#/documents">Документы</a></button> : null }
          {this.state.isAuthenticated ? <button class = "navButt"><a class="navLink text" href="#/profile">Профиль</a></button> : null}
        </nav>
        <div className="content">
          <Routes>
            <Route exact path="/" element={this.state.isAuthenticated ? <Profile refresh={this.refresh} setAuthenticated={this.setAuthenticated} /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/profile" element={this.state.isAuthenticated ? <Profile refresh={this.refresh} setAuthenticated={this.setAuthenticated}/> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
            <Route exact path="/documents" element={this.state.isAuthenticated ? <Documents refresh={this.refresh} /> : <Login setAuthenticated={this.setAuthenticated} />}></Route>
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