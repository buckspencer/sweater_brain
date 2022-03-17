import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

import { motion } from "framer-motion";
import './NavMenu.css';
export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

    render() {

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
            <Container>

            <NavbarBrand tag={Link} to="/">
                <motion.img
                    whileHover={{ scale: 1.3 }}
                    whileTap={{ scale: 0.9 }}
                    src={require("../logo/logo.svg").default}
                />
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                  <NavItem>
                    <motion.div
                        whileHover={{ scale: 1.3 }}
                        whileTap={{ scale: 0.9 }}>
                        <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                    </motion.div>
                  </NavItem>
                <NavItem>
                    <motion.div
                        whileHover={{ scale: 1.3 }}
                        whileTap={{ scale: 0.9 }}>
                        <NavLink tag={Link} className="text-dark" to="/sweater-brain">Suggester Core</NavLink>
                    </motion.div>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

