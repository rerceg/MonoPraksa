import React, { Component } from "react";

class PersonInput extends Component {
  state = {
    name: "",
    surname: "",
  };

  handleChange = (event) => {
    const value = event.target.value;
    if (event.target.name === "name") {
      this.setState({ name: value });
    } else {
      this.setState({ surname: value });
    }
  };

  handleSubmit = (event) => {
    event.preventDefault();
    this.props.createPerson(this.state.name, this.state.surname);
    this.setState({ name: "", surname: "" });
  };

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          Name:
          <input
            type="text"
            name="name"
            value={this.state.name}
            onChange={this.handleChange}
          />
        </label>
        <br />
        <label>
          Surname:
          <input
            type="text"
            name="surname"
            value={this.state.surname}
            onChange={this.handleChange}
          />
        </label>
        <br />
        <input type="submit" value="Create New Person" />
      </form>
    );
  }
}

export default PersonInput;
