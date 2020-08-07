import React, { Component } from "react";
import axios from "axios";
import Person from "./person";

class Persons extends Component {
  constructor() {
    super();
    this.state = {
      persons: [],
    };
  }

  componentDidMount() {
    axios
      .get("https://localhost:44333/api/Default/Persons")
      .then((response) => {
        this.setState({ persons: response.data });
      });
  }

  render() {
    let persons = this.state.persons.map((person) => {
      return (
        <Person key={person.Id} name={person.Name} surname={person.Surname} />
      );
    });
    return <div>{persons}</div>;
  }
}

export default Persons;
