import { useState } from "react";
import { Button, Offcanvas, Container, Row, Col } from 'react-bootstrap';
import bookMark from "../assets/svg/bookmark.svg";
import Cookies from 'js-cookie';


const BookmarkedOffcanvas = ({advertisementId, isSaved, ...props}) => {

    const [show, setShow] = useState(false);
    const [savedAdvertisements, setSavedAdvertisements] = useState([]);

    const handleClose = () => setShow(false);
    const handleSave = async () => {
      if(!isSaved) {
        // TODO: Add a status alert 
        await FetchApi(`https://localhost:7026/Advertisements/SaveAdvertisement/${advertisementId}`, "POST") 
      }
      setSavedAdvertisements(await FetchApi(`https://localhost:7026/Advertisements/GetSavedAdvertisements`, "GET"))
      setShow(true);
    }


    const FetchApi = async (url, httpMethod) => {
      
      try {
        const response = await fetch(url, {
          method: httpMethod,
          credentials: 'include',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${Cookies.get("Authorization")}`
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


    const handleDelete = async (savedAdvertisementId) => {
      await FetchApi(`https://localhost:7026/Advertisements/DeleteSavedAdvertisement/${savedAdvertisementId}`, "DELETE")
      setSavedAdvertisements(await FetchApi(`https://localhost:7026/Advertisements/GetSavedAdvertisements`, "GET"))
    }

    return (
        <>
      <Button variant={isSaved ? "danger" : "outline-danger"} onClick={handleSave} className="me-2" title="Mentés">
      <img loading='lazy' src={bookMark} alt="Mentés" />
      </Button>
      <Offcanvas show={show} onHide={handleClose} {...props}>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Mentett Hirdetések</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {
            savedAdvertisements && savedAdvertisements.length !== 0 ? savedAdvertisements.map(savedAdvertisement => 
              (
              <Container key={savedAdvertisement.id} fluid className="bg-secondary text-white rounded px-4 py-2 my-2">
                <Row>
                  <Col md={8}>
                    <Row>
                      <h5>CompanyName</h5>
                    </Row>
                    <Row>
                      <p>{savedAdvertisement.jobType}</p>
                    </Row>
                  </Col>
                  <Col md={4} className="d-flex align-items-center justify-content-end">
                    <Button onClick={() => handleDelete(savedAdvertisement.id)} variant="danger">Törlés</Button>
                  </Col>
                </Row>
              </Container>
              )
            ) :
            <Container>
              <p className="text-muted">Nincsenek mentett hirdetései.</p>
            </Container>
          }
        </Offcanvas.Body>
      </Offcanvas>
    </>
    );
}

export default BookmarkedOffcanvas;