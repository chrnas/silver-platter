
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import type { Restaurant } from '../Types/Restaurant';
import './css/popupRestaurantComp.css'
import { faStar } from '@fortawesome/free-solid-svg-icons';



function PopupRestaurantComp(props : { restaurant : Restaurant}) {

    return (
        <div className='Frame'>
            <div className="PopupRestaurant">
                <div className='Info'>
                    <div>
                        <h1 className='PR_Name'>{props.restaurant.name}</h1>
                        <p className='PR_Description'>{props.restaurant.description}</p>
                        <div className="badge" style={{backgroundColor: "rgba(0, 0, 0, 0.5)"}}>
                            {Array.from({length: props.restaurant.rating}).map((_, i) => (
                                <FontAwesomeIcon key={i} icon={faStar} />
                            ))}
                        </div>
                        <div className="badge" style={{color: 'white', backgroundColor: "rgba(0, 0, 0, 0.5)"}}>
                            {"<"}{props.restaurant.budget} SEK
                        </div>
                    </div>
                </div>
                <img
                    src="src/assets/pexels-life-of-pix-67468.jpg" 
                />
            </div>
        </div>
    )
}

export default PopupRestaurantComp;