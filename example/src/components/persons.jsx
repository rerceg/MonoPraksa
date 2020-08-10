import React, { Component } from "react";
import Person from "./person";

class Persons extends Component {
  render() {
    let persons = this.props.persons.map((person) => {
      return (
        <Person
          key={person.Id}
          person={person}
          onDelete={this.props.onDelete}
          getPersonsReceipts={this.props.getPersonsReceipts}
        />
      );
    });
    return <div>{persons}</div>;
  }
}

export default Persons;
