import React, { Component } from "react";

class Receipts extends Component {
  render() {
    let receipts = this.props.receipts.map((receipt) => {
      return (
        <div key={receipt.Id}>
          <p>Date: {receipt.Date}</p>
          <p>Total: {receipt.Total}</p>
          <hr />
        </div>
      );
    });
    return <div>{receipts}</div>;
  }
}

export default Receipts;
