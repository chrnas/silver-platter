import { Link } from 'react-router-dom';
import './css/Browse.css'

function Browse() {
    return (
        <div>
            <h1>Products Page</h1>
            <nav style={{ marginBottom: '20px' }}>
                <Link to="/products/car">Cars</Link> |{" "}
                <Link to="/products/bike">Bikes</Link>
            </nav> 
        </div>
    )
}

export default Browse;