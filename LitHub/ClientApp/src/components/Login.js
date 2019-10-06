import React, { Component } from 'react';

export class Login extends Component {
    displayName = Login.name    

  constructor(props) {
    super(props);
      this.state = { username: null, password: null, signed: false, error_text: null, loading: false };
      this.login = this.login.bind(this)
      this.get_content = this.get_content.bind(this)
      this.handleInputChange = this.handleInputChange.bind(this)
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    login() {
        const _api = 'api/User/Login'
        const _send_data = { username: this.state.username, password: this.state.password }
        this.setState({ loading: true });
        try {
            fetch(_api, {
                method: 'POST',
                body: JSON.stringify(_send_data),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    console.log('OK:', JSON.stringify(data));
                    this.setState({ signed: data.signed, loading: false });
                });
        }
        catch (error) {
            console.error('Ошибка:', error);
            this.setState({ signed: false, loading: false, error_text: error });
        }
        this.render();
    }
    
    get_content() {
        if (this.state.loading) {
            return (<div><p><em>Loading...</em></p></div>);
        }
        else if (this.state.signed) {
            return (
                <div>
                    <p>Succesfully signed... </p>
                    <p>Move to <a href="/">Home Page</a></p>
                </div>);
        }
        else {
            return (
                <div>
                <h1>Sign Up on lithub or <a href="/register">Register</a></h1>
                <p>Enter username and password</p>
                <form>
                    <div class="form-group">
                        <label for="UserNameField">Login</label>
                            <input type="text"
                                class="form-control"
                                id="UserNameField"
                                name="username"
                                value={this.state.username}
                                onChange={this.handleInputChange}
                                placeholder="Enter login" />
                    </div>
                    <div class="form-group">
                        <label for="PasswordField">Password</label>
                            <input type="password"
                                class="form-control"
                                id="PasswordField"
                                name="password"
                                value={this.state.password}
                                onChange={this.handleInputChange}
                                placeholder="Password" />
                    </div>
                        <button onClick={this.login} class="btn btn-primary">Sigh Up</button>
                    </form>
                </div>);
        }
    }

    render() {     
      return (this.get_content());
  }
}
