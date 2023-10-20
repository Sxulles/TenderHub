import { useState } from "react";
import { Button, Offcanvas, Container, Row, Col } from 'react-bootstrap';
import bookMark from "../assets/svg/bookmark.svg";
import Cookies from 'js-cookie';


const BookmarkedOffcanvas = ({user, advertisementId, name, ...props}) => {

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleSave = async () => {
      console.log(Cookies.get("Authorization"))

      var isSavingSuccesful = await FetchApi(`https://localhost:7026/Advertisement/SaveAdvertisement/user=${user}&advertisement=${advertisementId}`, "POST") 
      setSavedAdvertisements(await FetchApi(`https://localhost:7026/Advertisement/GetSavedAdvertisements/user=${user}`, "GET"))

      setShow(true);
    }


    const [savedAdvertisements, setSavedAdvertisements] = useState([]);


    const FetchApi = async (url, httpMethod) => {
      const token = Cookies.get("Authorization");
      
      try {
        const response = await fetch(url, {
          method: httpMethod,
          credentials: 'include',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          }
        });
    
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
    
        const data = await response.json();
        return data;

      } catch (error) {
        console.error('Error while fetching data:', error);
        throw error; // Important: rethrow the error for proper handling
      }
    };





    return (
        <>
      <Button variant="danger" onClick={handleSave} className="me-2" title="Mentés">
      <img loading='lazy' src={bookMark} alt="Mentés" />
      </Button>
      <Offcanvas show={show} onHide={handleClose} {...props}>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Mentett Hirdetések</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {
            savedAdvertisements && savedAdvertisements.map(advertisement => 
              (
              <Container key={advertisement.id} fluid className="bg-secondary text-white rounded px-4 py-2 my-2">
                <Row>
                  <Col md={8}>
                    <Row>
                      <h5>CompanyName</h5>
                    </Row>
                    <Row>
                      <p>{advertisement.jobType}</p>
                    </Row>
                  </Col>
                  <Col md={4} className="d-flex align-items-center justify-content-end">
                    <Button variant="danger">Törlés</Button>
                  </Col>
                </Row>
              </Container>
              )
              )
          }
        </Offcanvas.Body>
      </Offcanvas>
    </>
    );
}
 
export default BookmarkedOffcanvas;