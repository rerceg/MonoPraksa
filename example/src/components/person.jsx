import React, { Component } from "react";

class Person extends Component {
  constructor(props) {
    super(props);
    this.state = {
      name: props.name,
      surname: props.surname,
      id: props.id,
    };
  }

  render() {
    return (
      <div>
        <span>
          {this.state.name} {this.state.surname}
        </span>
        <button>Delete</button>
        <button>Update</button>
        <button>Receipts</button>
      </div>
    );
  }
}

export default Person;
