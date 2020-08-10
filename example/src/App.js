import React, { Component } from "react";
import Persons from "./components/persons";
import PersonInput from "./components/personInput";
import axios from "axios";
import Receipts from "./components/receipts";

class App extends Component {
  constructor() {
    super();
    this.state = {
      persons: [],
      receipts: [],
    };
  }

  componentDidMount() {
    this.getPersons();
  }

  handleDelete = async (id) => {
    await axios.delete("https://localhost:44333/api/Default/Persons?id=" + id);
    this.getPersons();
  };

  getPersons() {
    axios
      .get("https://localhost:44333/api/Default/Persons")
      .then((response) => {
        this.setState({ persons: response.data });
      });
  }

  getPersonsReceipts = async (id) => {
    try {
      await axios
        .get("https://localhost:44333/api/Default/PersonsReceipts?id=" + id)
        .then((response) => {
          this.setState({ receipts: response.data.m_Item2 });
        });
    } catch (err) {
      this.setState({ receipts: [] });
    }
  };

  createPerson = async (name, surname) => {
    await axios.post(
      "https://localhost:44333/api/Default/Persons?name=" +
        name +
        "&surname=" +
        surname
    );
    this.getPersons();
  };

  render() {
    return (
      <div className="App">
        <Persons
          persons={this.state.persons}
          onDelete={this.handleDelete}
          getPersonsReceipts={this.getPersonsReceipts}
        />
        <hr />
        <PersonInput createPerson={this.createPerson} />
        <hr />
        <Receipts receipts={this.state.receipts} />
      </div>
    );
  }
}

export default App;
