import React, { Component } from "react";

class Person extends Component {
  render() {
    return (
      <div>
        <span>
          {this.props.person.Name} {this.props.person.Surname}
        </span>
        <button onClick={() => this.props.onDelete(this.props.person.Id)}>
          Delete
        </button>
        <button>Update</button>
        <button
          onClick={() => this.props.getPersonsReceipts(this.props.person.Id)}
        >
          Receipts
        </button>
      </div>
    );
  }
}

export default Person;
