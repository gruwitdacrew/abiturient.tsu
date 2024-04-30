import React, { Component } from "react";
import './style/common.css';

class Register extends Component {
  render() {
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
            <input class = "text" type="email" id="email" />
          </div>


          <div class="form-column">
            <label class = "text" for="password">Password</label>
            <input class = "text" type="password" id="password" />
          </div>

          <div class="form-column">
            <label class = "text" for="password-verify">Verify password</label>
            <input class = "text" type="password" id="password-verify" />
          </div>
          
          <button type="submit" class="submit text">Sign up</button>
        </form>

      </div>
    );
  }
}

export default Register;