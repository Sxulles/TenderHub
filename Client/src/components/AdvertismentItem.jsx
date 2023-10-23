import { Container, Row, Col, Accordion, Table, Button, ButtonGroup, Badge } from 'react-bootstrap';
import { useState } from 'react';
import OfferModal from './OfferModal';
import BookmarkedOffcanvas from './BookmarkedOffcanvas';
import stars from "../assets/svg/stars.svg"

const AdvertismentItem = ({advertisement, index}) => {

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const formatDateTime = (dateTime) => {
    let date = new Date(dateTime);

    let year = date.getFullYear();
    let month = date.getMonth()+1;
    let day = date.getDate();

    return `${year}.${month}.${day}`
  };

    return (
      <Accordion.Item eventKey={index}>
        <Accordion.Header>
          <Container className='p-3'>
            <Row>
              <Col className="text-center">{advertisement.isHighlighted ? <Badge pill bg="warning">Kiemelt<img src={stars}/></Badge> : <Badge pill bg="info">asd</Badge>}</Col>
              <Col className="text-center">{advertisement.advertiser}</Col>
              <Col className="text-center">{advertisement.jobType}</Col>
              <Col className="text-center">{advertisement.location.city}</Col>
              <Col className="text-center">{formatDateTime(advertisement.deadlineStart)}</Col>
              <Col className="text-center">{formatDateTime(advertisement.deadlineEnd)}</Col>
              <Col className="text-center text-success fw-bold">{advertisement.status}</Col>
            </Row>
          </Container>
        </Accordion.Header>
        <Accordion.Body className="bg-light border border-primary">
        <Container>
          <Table responsive>
            <thead>
              <tr>
                <th className='text-start p-3'>Munka részletei</th>
                <th className='text-center p-3'>Anyagmennyiség</th>
              </tr>
            </thead>
            <tbody>
              {
                advertisement && advertisement.jobtasks.map((jobtask, index) => (
                    <tr key={index}>
                      <td className='text-start align-middle p-3'>{jobtask.description}</td>
                      <td className='text-center align-middle p-3'>{`${jobtask.surface.x*jobtask.surface.y}${jobtask.surface.unit}`}</td>
                      
                    </tr>
                  )
                )
              }
            </tbody>
          </Table>
          <div className='container-fluid d-flex justify-content-end'>
            <ButtonGroup>
              <Button variant='outline-primary' onClick={handleShow}>Ajánlat küldése</Button>
              <BookmarkedOffcanvas scroll={"true"} placement={"end"} advertisementId={advertisement.id} isSaved={advertisement.isSavedByUser}/>
            </ButtonGroup>
            <OfferModal show={show} handleClose={handleClose} advertisementDetails={advertisement}/>
          </div>
          </Container>
        </Accordion.Body>
      </Accordion.Item>
    );
}
 
export default AdvertismentItem;