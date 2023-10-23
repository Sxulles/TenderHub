import { useState, useEffect } from 'react';
import Accordion from 'react-bootstrap/Accordion';
import AdvertismentItem from './AdvertismentItem';
import { Container, Row, Col, Button } from 'react-bootstrap';
import arrows from "../assets/svg/arrow-down-up.svg"
import Cookies from 'js-cookie';

const Advertisments = () => {

    const [advertisements, setAdvertisements] = useState([]);

    useEffect(() => {
        FetchApi();
    }, []);

    const FetchApi = () => {
        const token = Cookies.get("Authorization");
      try {
          fetch('https://localhost:7026/Advertisements/GetAdvertisements', {
            method: 'GET',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
          })
              .then((response) => {
                  if (!response.ok) {
                      throw new Error(`HTTP error! status: ${response.status}`);
                  }
                  return response.json();
              })
              .then((data) => {
                setAdvertisements(data);
                console.log(advertisements);
              })
              .catch((error) => {
                  console.error('Error while fetching data:', error);
              });
          
      } catch (error) {
          console.error('Error fetching data:', error);
      }
    };

    return (
        <div className="bg-light py-5">
            <Container>
                <div className="mb-16 text-center"><span className="fs-9 fw-semibold text-primary text-uppercase">DEMO</span>
                    <h1 className="mt-8 mb-5">Aktuális Hirdetések</h1>
                    <Button onClick={() => FetchApi()}>Refresh</Button>
                </div>

                <Container>
                    <Row className='view-padding fw-bold'>
                        <Col className="text-center border-end"><h5>Tipus</h5></Col>
                        <Col className="text-center border-end"><h5>Cég <img src={arrows}/></h5></Col>
                        <Col className="text-center border-end"><h5>Munkanem <img src={arrows}/></h5></Col>
                        <Col className="text-center border-end"><h5>Lokáció <img src={arrows}/></h5></Col>
                        <Col className="text-center border-end"><h5>Határidő kezdete <img src={arrows}/></h5></Col>
                        <Col className="text-center border-end"><h5>Határidő vége <img src={arrows}/></h5></Col>
                        <Col className="text-center pe-4 me-2 border-start"><h5>Státusz <img src={arrows}/></h5></Col>
                    </Row>
                </Container>



                <Accordion>
                    {
                    advertisements &&
                        advertisements.map((advertisement, index) => (
                            <AdvertismentItem advertisement={advertisement} index={index} key={index} />
                        ))
                    }
                </Accordion>

            </Container>
      </div>
    );
}
 
export default Advertisments;