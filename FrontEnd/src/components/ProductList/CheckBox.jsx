import { useState } from 'react';
import './checkboxes.scss';

const CheckBox = () => {
    const [checkboxes, setCheckboxes] = useState({
        Small: false,
        Medium: false,
        Large: false,
    });
    const handleCheckboxChange = (event) => {
        const { name } = event.target;
        setCheckboxes({
            Small: name === 'Small' ? true : false,
            Medium: name === 'Medium' ? true : false,
            Large: name === 'Large' ? true : false,
        });
    };
    return (
        <div className='checkboxContainer'>
            <label>
                <input
                    type="checkbox"
                    name="Small"
                    checked={checkboxes.Small}
                    onChange={handleCheckboxChange}
                />
                Small
            </label>
            <label>
                <input
                    type="checkbox"
                    name="Medium"
                    checked={checkboxes.Medium}
                    onChange={handleCheckboxChange}
                />
                Medium
            </label>
            <label>
                <input
                    type="checkbox"
                    name="Large"
                    checked={checkboxes.Large}
                    onChange={handleCheckboxChange}
                />
                Large
            </label>
        </div>
    );
};

export default CheckBox;
