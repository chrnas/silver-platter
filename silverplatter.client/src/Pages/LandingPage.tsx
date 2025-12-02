import './css/LandingPage.css'

function LandingPage() {


    return (
        <div id="LandingPage">
            <div id="Content">
                <img 
                    src="src/assets/claim-your-i-was-here-button-v0-9tlo8368wdkf1.webp" 
                />
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