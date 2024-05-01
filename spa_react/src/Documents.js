import React, { Component } from "react";

class Documents extends Component {
  render() {
    return (
      <div class = "container">

        <div class="form-row">
          <form id="passport" class = "form">
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

          <form id="education" class = "form">
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
      </div>
    );
  }
}

export default Documents;