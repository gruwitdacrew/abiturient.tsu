import React, { Component } from "react";
import './style/common.css';
import Documents from "./Documents";
import Login from "./Login";

class Register extends Component {
  register = async () => {
    if (document.getElementById("email").value === "" || document.getElementById("phone").value === "" || document.getElementById("password").value === "" || document.getElementById("fullName").value === "")
    {
      alert("Заполните все поля");
    }
    else if (document.getElementById("password-verify").value !== document.getElementById("password").value)
    {
      alert("Пароли не совпадают");
    }
    else fetch(window.users + "/api/users/register", {
      method: 'POST',
      body: JSON.stringify({  email: document.getElementById("email").value,
                              phone: document.getElementById("phone").value,
                              fullName: document.getElementById("fullName").value,
                              password: document.getElementById("password").value,
                            }),
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
  toLogin = async () => {
    this.setState({ redirectToLogin: true });
  };

render() {

    if (this.state && this.state.redirectToDocuments) {
      return <Documents />;
  }
    if (this.state && this.state.redirectToLogin) {
      return <Login />;
  }

    return (
      <div class = "container">

        <form id="signup-form" class = "form">
          <div id="alert"></div>

          <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Sign Up</label>

          <div class="form-column">
            <label class = "text" for="email">Email</label>
            <input class = "text" type="email" id="email" />
          </div>

          <div class="form-column">
            <label class = "text" for="phone">Phone</label>
            <input class = "text" type="phone" id="phone" />
          </div>

          <div class="form-column">
            <label class = "text" for="fullName">Name</label>
            <input class = "text" type="fullName" id="fullName" />
          </div>

          <div class="form-column">
            <label class = "text" for="password">Password</label>
            <input class = "text" type="password" id="password" />
          </div>

          <div class="form-column">
            <label class = "text" for="password-verify">Verify password</label>
            <input class = "text" type="password" id="password-verify" />
          </div>

          <a href="#/login" onClick={this.toLogin}>Уже зарегистрированы?</a>
          
          <button type="submit" class="submit text" onClick={this.register}>Sign up</button>
        </form>

      </div>
    );
  }
}

export default Register;