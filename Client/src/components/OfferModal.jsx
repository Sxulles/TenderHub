import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

const OfferModal = ({show, handleClose, advertisementDetails}) => {
    return (
        <Modal
        fullscreen={true}
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            <h1>{`${advertisementDetails.applicationUser.companyName} - ${advertisementDetails.jobType}`}</h1>
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h4>Details</h4>
          <p>
            Cras mattis consectetur purus sit amet fermentum. Cras justo odio,
            dapibus ac facilisis in, egestas eget quam. Morbi leo risus, porta ac
            consectetur ac, vestibulum at eros.
          </p>
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={handleClose} variant='danger'>Mégse</Button>
          <Button variant='success'>Küldés</Button>
        </Modal.Footer>
      </Modal>
    );
}
 
export default OfferModal;