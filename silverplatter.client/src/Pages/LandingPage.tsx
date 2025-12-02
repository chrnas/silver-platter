import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import './css/LandingPage.css'

function LandingPage() {


    return (
        <div id="LandingPage">
            <div id="Content">
                <PopupRestaurantComp/>
            </div>

            <div id="Background">
                <video autoPlay muted loop> 
                    <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
                </video>
            </div>
        </div>
    )
}

export default LandingPage;