import React, { Component } from 'react';

export class Hub extends Component {
    
    static renderHub(hub) {
        return (
            <div>
                <p>Name: {hub.name}</p>
                <p>Description: {hub.description}</p>
                <p>Author: {hub.author}</p>
                <p>Files:</p>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>DateModified</th>
                            <th>Author</th>
                            <th>Description</th>
                            <th/>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {hub.files.map(file => {
                            <tr key={file.id}>
                                <td>{file.name}</td>
                                <td>{file.dateModified}</td>
                                <td>{file.author}</td>
                                <td>{file.description}</td>
                                <td><a href={"/hub/file/?id=" + file.id}><span className="glyphicon glyphicon-eye-open" aria-hidden="true"/></a></td>
                                <td><a href={"/hub/file/?id=" + file.id}><span className="glyphicon glyphicon-edit" aria-hidden="true"/></a></td>
                            </tr>;
                        }
                        )}
                    </tbody>
                </table>
                <p><a href="/"><span className="glyphicon glyphicon-home" aria-hidden="true">Back</span></a></p>
            </div>
        );
    }

    displayName = Hub.name;  

    constructor(props) {
        super(props);
        const queryString = require('query-string');
        var parsed = queryString.parse(this.props.location.search);
        this.state = { id: parsed.id, loading: true, hub: null };
        this.getHub = this.getHub.bind(this);
        this.get_content = this.get_content.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.getHub();
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    getHub() {
        const _api = 'api/Hub/' + this.state.id;        
        this.setState({ loading: true });        
        try {
            fetch(_api)
                .then(response => response.json())
                .then(data => {
                    console.log('OK:', JSON.stringify(data));
                    this.setState({ hub: data, loading: false });
                });
        }
        catch (error) {
            console.error('Ошибка:', error);
            this.setState({ hub: null, loading: false, error_text: error });
        }
        this.render();
    }
    
    get_content() {
        if (this.state.loading) {
            return <div><p><em>Loading...</em></p></div>;
        }        
        else {
            return Hub.renderHub(this.state.hub);
        }
    }

    render() {     
      return this.get_content();
  }
}
