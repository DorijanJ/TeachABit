import React, { useState } from 'react';
import Slider from 'rc-slider';
import 'rc-slider/assets/index.css';

interface PriceRangeSelectorProps {
    min: number;
    max: number;
    onChange: (value: [number, number]) => void;
}

const PriceRangeSelector: React.FC<PriceRangeSelectorProps> = ({ min, max, onChange }) => {
    const [range, setRange] = useState<[number, number]>([min, max]);

    const handleChange = (value: [number, number]) => {
        setRange(value);
        onChange(value);
    };

    return (
        <div>
            <Slider
                range
                min={min}
                max={max}
                defaultValue={[min, max]}
                onChange={handleChange}
                value={range}
            />
        </div>
    );
};

export default PriceRangeSelector;
