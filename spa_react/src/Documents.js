import React, { Component } from "react";

class Documents extends Component {
  componentDidMount() {
    // Выполнение запроса при загрузке страницы
    fetch(window.server + "/api/users/profile", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.componentDidMount()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data) {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      else{
        document.getElementById("email").value = data.email;
        document.getElementById("phone").value = data.phone;
        document.getElementById("fullName").value = data.fullName
        document.getElementById("birthDate").value = data.birthDate
        document.getElementById("gender").value = data.gender
        document.getElementById("nationality").value = data.nationality
        document.getElementById("role").value = data.roles.join(" | ");
      }
    });
  }
  render() {
    return (
      <div class = "container">

        <div class="form-row">
          <form id="passport" class = "form">
            <div id="alert"></div>

            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Документ об образовании</label>

            <div class="form-column">
              <label class = "text" for="education">Email</label>
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

            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Паспорт</label>

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