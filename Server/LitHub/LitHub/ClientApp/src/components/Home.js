import React, { Component } from 'react';

export class Home extends Component {

    static renderHubTable(hubs) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>DateModified</th>
                        <th>Author</th>
                        <th/>
                    </tr>
                </thead>
                <tbody>
                    <tr key="111">
                        <td>test</td>
                        <td>test</td>
                        <td>test</td>
                        <td>test</td>
                    </tr>
                    {hubs.map(hub => {
                        <tr key="111">
                            <td>test</td>
                            <td>test</td>
                            <td>test</td>
                            <td>test</td>
                        </tr>;
                    }
                    )}
                </tbody>
            </table>
        );
    }

    displayName = Home.name

    constructor(props) {
        super(props);
        this.state = { signed: true, loading: true, hubs: [] };       
        //this.getHubs();
        fetch('api/Hub')
            .then(response => response.json())
            .then(data => {
                this.setState({ hubs: data, loading: false });
            });
        this.get_content = this.get_content.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);        
    }

    getHubs() {
        if (this.state.signed) {
            this.setState({ loading: true });
            //this.render();
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
    
    get_content() {
        if (this.state.loading) {
            return <div><p><em>Loading...</em></p></div>;
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
      var ret = this.get_content();  
      return ret;
  }
}
