import React, { Component } from "react";
import Documents from "./Documents";
import './style/common.css';
import Register from "./Register";


class Login extends Component {
  login = async () => {
      if (document.getElementById("email").value === "" || document.getElementById("password").value === "")
      {
        alert("Заполните все поля");
      }
      else fetch(window.server + "/api/users/login", {
        method: 'POST',
        body: JSON.stringify({ email: document.getElementById("email").value, password: document.getElementById("password").value}),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => {
        if (response.status !== 500) return response.json();
      })
      .then(data => {
        if (data.statusCode) {
          alert(data.message);
        }
        else{
          localStorage.setItem("accessToken", data.accessToken);
          localStorage.setItem("refreshToken", data.refreshToken);
    
          this.props.setAuthenticated(true);
          this.setState({ redirectToDocuments: true });
        }
      });
  };
  toRegister = async () => {
    this.setState({ redirectToRegister: true });
  };


  render() {

    if (this.state && this.state.redirectToDocuments) {
      return <Documents />;
  }

    if (this.state && this.state.redirectToRegister) {
      return <Register />;
  }

    return (
      <div class = "container">

        <form id="login-form" class = "form">
          <div id="alert"></div>

          <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Login</label>

          <div class="form-column">
            <label class = "text" for="email">Email</label>
            <input class = "text" type="email" id="email" />
          </div>

          <div class="form-column">
            <label class = "text" for="password">Password</label>
            <input class = "text" type="password" id="password" />
          </div>

          <a href="#/register" onClick={this.toRegister}>Регистрация</a>
          
          <button type="submit" class="submit text" onClick={this.login}>OK</button>
        </form>
        
      </div>
    );
  }
}

export default Login;