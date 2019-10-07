import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    constructor(props) {
        super(props);
        this.state = { signed: true, loading: true, hubs: [] };        
        this.get_content = this.get_content.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.getHubs();
    }

    getHubs() {
        if (this.state.signed) {
            this.setState({ loading: true });
            this.render();
            fetch('api/Hub')
                .then(response => response.json())
                .then(data => {
                    this.setState({ hubs: data, loading: false });
                });
            this.render();
        }        
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    static renderHubTable(hubs) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>DateModified</th>
                        <th>Author</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {hubs.map(hub =>
                        <tr key={hub.id}>
                            <td>{hub.name}</td>
                            <td>{hub.dateModified}</td>
                            <td>{hub.author}</td>
                            <td><a href={"/hub/?id=" + hub.id }><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></a></td>                           
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    get_content() {
        if (this.state.loading) {
            return (<div><p><em>Loading...</em></p></div>);
        }
        else if (this.state.signed) {
            return  Home.renderHubTable(this.state.hubs);
        }
        else {
            return (
                <div>
                    <h1>LitHub WebClient</h1>
                    <p>Simple tool for art teamwork and version contol.</p>
                    <p>To get started follow next four steps:</p>
                    <ul>
                        <li><a href="/register">Register</a></li>
                        <li>Create new or fork exists project</li>
                        <li>Edit project file(s) in editor on PC or browser editor</li>
                        <li>Save work</li>
                    </ul>
                    <p>Profit!</p>
                </div>);
        }
    }

  render() {
      return (this.get_content());  
  }
}
