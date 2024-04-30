import React, { Component } from "react";
import Documents from "./Documents";

class Login extends Component {
  handleLogin = async () => {
    try {
      const response = await fetch('https://your-authentication-endpoint', {
        method: 'POST',
        body: JSON.stringify({ username: 'example', password: 'example' }),
        headers: {
          'Content-Type': 'application/json'
        }
      });

      if (response.ok)
      {
        this.props.setIsAuthenticated(true);
        this.setState({ redirectToDocuments: true });
      }
      else
      {
        console.error('Authentication failed');
      }
    }
    catch (error)
    {
      console.error('Error:', error);
    }
  };

  render() {

    if (this.state.redirectToDocuments) {
      return <Documents />;
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
          
          <button type="submit" class="submit text" onClick={this.handleLogin}>OK</button>
        </form>
        
      </div>
    );
  }
}

export default Login;