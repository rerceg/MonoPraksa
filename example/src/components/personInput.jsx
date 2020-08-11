import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";

const PersonInput = observer(
  class PersonInput extends Component {
    name = "";
    surname = "";

    handleChange = (event) => {
      const value = event.target.value;
      if (event.target.name === "name") {
        this.name = value;
      } else {
        this.surname = value;
      }
    };

    handleSubmit = (event) => {
      event.preventDefault();
      this.props.createPerson(this.name, this.surname);
      this.name = "";
      this.surname = "";
    };

    render() {
      return (
        <form onSubmit={this.handleSubmit}>
          <label>
            Name:
            <input
              type="text"
              name="name"
              value={this.name}
              onChange={this.handleChange}
            />
          </label>
          <br />
          <label>
            Surname:
            <input
              type="text"
              name="surname"
              value={this.surname}
              onChange={this.handleChange}
            />
          </label>
          <br />
          <input type="submit" value="Create New Person" />
        </form>
      );
    }
  }
);

decorate(PersonInput, {
  name: observable,
  surname: observable,
});

export default PersonInput;
