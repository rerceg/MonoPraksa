import React, { Component } from "react";
import Persons from "./components/persons";
import PersonInput from "./components/personInput";
import axios from "axios";
import Receipts from "./components/receipts";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";

const App = observer(
  class App extends Component {
    persons = [];
    receipts = [];

    componentDidMount() {
      this.getPersons();
    }

    handleDelete = async (id) => {
      await axios.delete(
        "https://localhost:44333/api/Default/Persons?id=" + id
      );
      this.getPersons();
    };

    getPersons() {
      axios
        .get("https://localhost:44333/api/Default/Persons")
        .then((response) => {
          this.persons = response.data;
        });
    }

    getPersonsReceipts = async (id) => {
      try {
        await axios
          .get("https://localhost:44333/api/Default/PersonsReceipts?id=" + id)
          .then((response) => {
            this.receipts = response.data.m_Item2;
          });
      } catch (err) {
        this.receipts = [];
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
            persons={this.persons}
            onDelete={this.handleDelete}
            getPersonsReceipts={this.getPersonsReceipts}
          />
          <hr />
          <PersonInput createPerson={this.createPerson} />
          <hr />
          <Receipts receipts={this.receipts} />
        </div>
      );
    }
  }
);

decorate(App, {
  persons: observable,
  receipts: observable,
});

export default App;
