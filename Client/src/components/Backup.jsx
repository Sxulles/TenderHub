import { Container, Row, Col, Accordion, Table, Form } from 'react-bootstrap';
import { useState, useEffect } from 'react';

const AdvertismentItem = ({advertisement, index}) => {

  const initialFeesState = advertisement.jobs.reduce((acc, _, index) => {
    acc[`materialFee${index}`] = 0;
    acc[`workFee${index}`] = 0;
    acc[`totalFee${index}`] = 0;
    return acc;
  }, {});
  
  

  const [fees, setFees] = useState(initialFeesState);

  const handleOnChange = (target) => {
    const { name, value } = target;
    setFees((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  useEffect(() => {
    console.log(fees);
    // Calculate totalFee for each job and update the state.
    const updatedFees = { ...fees };
    let totalUpdated = false; // Flag to track if totalFee was updated.
  
    advertisement.jobs.forEach((_, index) => {
      const materialFee = parseFloat(updatedFees[`materialFee${index}`] || 0);
      const workFee = parseFloat(updatedFees[`workFee${index}`] || 0);
      const totalFee = (materialFee + workFee);
  
      if (updatedFees[`totalFee${index}`] !== totalFee) {
        updatedFees[`totalFee${index}`] = totalFee;
        totalUpdated = true;
      }
    });
  
    // Only update the state if totalFee has changed.
    if (totalUpdated) {
      setFees(updatedFees);
    }
  }, [fees, advertisement.jobs]); 

    return (
      <Accordion.Item eventKey={index}>
        <Accordion.Header>
          <Container className='p-3'>
            <Row>
              <Col className="text-center">{advertisement.companyName}</Col>
              <Col className="text-center">{advertisement.jobType}</Col>
              <Col className="text-center">{advertisement.location}</Col>
              <Col className="text-center">{advertisement.deadlineStart}</Col>
              <Col className="text-center">{advertisement.deadlineEnd}</Col>
              <Col className="text-center text-success fw-bold">{advertisement.status}</Col>
            </Row>
          </Container>
        </Accordion.Header>
        <Accordion.Body className="bg-light border border-primary">
        <Container>
          <Table responsive>
            <thead>
              <tr>
                <th className='text-start p-3'>Hirdetett munkák</th>
                <th className='text-center p-3'>Mennyiség</th>
                <th className='text-center p-3'>Mértékegység</th>
                <th className='text-center p-3'>Anyagdíj</th>
                <th className='text-center p-3'>Munkadíj</th>
                <th className='text-center p-3'>Végösszeg</th>
              </tr>
            </thead>
            <tbody>
              {
                advertisement && advertisement.jobs.map((job, index) => (
                    <tr key={index}>
                      <td className='text-start align-middle p-3'>{job.description}</td>
                      <td className='text-center align-middle p-3'>1200</td>
                      <td className='text-center align-middle p-3'>m2</td>
                      <td className='align-middle p-3'><Form.Control className='mx-auto text-center' name={`materialFee${index}`} value={fees[`materialFee${index}`]} type="number" onChange={(e) => handleOnChange(e.target)} placeholder="Ft"/></td>
                      <td className='align-middle p-3'><Form.Control className='mx-auto text-center' name={`workFee${index}`} value={fees[`workFee${index}`]} type="number" onChange={(e) => handleOnChange(e.target)} placeholder="Ft"/></td>
                      <td className='align-middle p-3'><Form.Control className='mx-auto text-center' name={`totalFee${index}`} readOnly={true} value={fees[`totalFee${index}`]} type="number" placeholder="Ft"/></td>
                    </tr>
                  )
                )
              }
            </tbody>
          </Table>
          </Container>
        </Accordion.Body>
      </Accordion.Item>
    );
}
 
export default AdvertismentItem;