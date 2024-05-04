import React, { Component } from "react";

class Documents extends Component {
  componentDidMount() {
    // Выполнение запроса при загрузке страницы
    fetch(window.documents + "/api/document/education", {
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
      if (response.status === 404)
      {
        document.getElementById('createEducation').style.display = "block";
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
        document.getElementById('inputsEducation').style.display = "block";
        document.getElementById('documentType').value = data.documentType;
        document.getElementById('number_edu').value = data.number;
        document.getElementById('date_edu').value = data.date;
        document.getElementById('grade').value = data.grade;
      }
    });
  }

  createEducation = async() =>
  {
    fetch(window.documents + "/api/document/education", {
      method: 'POST',
      body: JSON.stringify({  documentType: document.getElementById('documentType').value,
                              number: document.getElementById('number_edu').value, 
                              date: document.getElementById('date_edu').value, 
                              grade: document.getElementById('grade').value, 
                            }),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.createEducation()});
        return;
      }
      else if (response.status === 200)
      {

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
    });
  }

  render() {
    return (
      <div class = "container">

        <div class="form-row">
          <form class = "form">
            <div id="alert"></div>
            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Документ об образовании</label>
            <div id="inputsEducation" style={{display: "none"}}>

              <div class="form-column">
                <label class = "text" for="documentType">Тип документа</label>
                <input class = "text" id="documentType" />
              </div>

              <div class="form-column">
                <label class = "text" for="number_edu">Номер документа</label>
                <input class = "text" id="number_edu" />
              </div>

              <div class="form-column">
                <label class = "text" for="date_edu">Дата выдачи</label>
                <input class = "text" id="date_edu" />
              </div>

              <div class="form-column">
                <label class = "text" for="grade">Оценка</label>
                <input class = "text" id="grade" />
              </div>
              
              <button type="submit" class="submit text" onClick={this.createEducation()}>OK</button>

            </div>

            <button id="createEducation" type="submit" class="submit text" style={{backgroundColor:"blue", display:"none"}} onClick="document.getElementById('inputsEducation').style.display = 'block'; document.getElementById('createEducation').style.display = 'none';">Создать</button>
          </form>

          <form class = "form">
            <div id="alert"></div>
            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Паспорт</label>
            <div id="inputsPassport">

              <div class="form-column">
                <label class = "text" for="email">Email</label>
                <input class = "text" type="email" id="email" />
              </div>

              <div class="form-column">
                <label class = "text" for="password">Password</label>
                <input class = "text" type="password" id="password" />
              </div>
            </div>
            
            <button type="submit" class="submit text">OK</button>

            <button id="createPassport" type="submit" class="submit text" style={{backgroundColor:"blue", display:"none"}}>Создать</button>
          </form>
        </div>
      </div>
    );
  }
}

export default Documents;