import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from 'react-router-dom'

const NavMenu = () => {
    return (
        <Navbar collapseOnSelect expand="lg" className="bg-body-tertiary border-bottom">
      <Container>
        <Navbar.Brand href="#home">TenderHub</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/advertisements">Advertisements</Nav.Link>
          </Nav>
          <Nav>
            <Nav.Link as={Link} to="/login">Sign In</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    );
}
 
export default NavMenu;