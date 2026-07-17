import http from "k6/http";
import { check, sleep } from "k6";

const binFile = open("./test_image.png", "b");

const imageWidth = 250;
const imageHeight = 250;

const transforms = [
    "rotate-cw",
    "rotate-ccw",
    "flip-v",
    "flip-h"
];

const xRps = 1000;

export const options = {
    scenarios: {
        scenario_1_ramping: {
            executor: "ramping-arrival-rate",
            startRate: 1,
            timeUnit: "1s",
            preAllocatedVUs: 100,
            maxVUs: 2000,
            stages: [
                { target: 10000, duration: "10m" }
            ],
            exec: "imageProcessingTest"
        },

        scenario_2_constant: {
            executor: "constant-arrival-rate",
            rate: Math.floor(xRps * 0.9),
            timeUnit: "1s",
            duration: "5m",
            preAllocatedVUs: 200,
            maxVUs: 3000,
            exec: "imageProcessingTest"
        },

        scenario_3_spikes: {
            executor: "ramping-arrival-rate",
            startRate: Math.floor(xRps * 0.9),
            timeUnit: "1s",
            preAllocatedVUs: 500,
            maxVUs: 5000,
            stages: [
                { target: Math.floor(xRps * 0.9), duration: "2m" },
                { target: xRps * 10, duration: "10s" },
                { target: Math.floor(xRps * 0.9), duration: "10s" },
                { target: Math.floor(xRps * 0.9), duration: "3m" }
            ],
            exec: "imageProcessingTest"
        },

        scenario_4_constantX2: {
            executor: "constant-arrival-rate",
            rate: Math.floor(xRps * 2),
            timeUnit: "1s",
            duration: "5m",
            preAllocatedVUs: 200,
            maxVUs: 3000,
            exec: "imageProcessingTest"
        }

    },

    thresholds: {
        http_req_failed: ["rate<0.01"]
    }
};


export function randomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

export function randomTransform() {
    return transforms[randomInt(0, transforms.length - 1)];
}

export function randomCoords() {
    const x = randomInt(0, imageWidth - 1);
    const y = randomInt(0, imageHeight - 1);
    const w = randomInt(1, imageWidth - x);
    const h = randomInt(1, imageHeight - y);

    return `${x},${y},${w},${h}`;
}

export function imageProcessingTest() {
    const transform = randomTransform();
    const coords = randomCoords();

    const url = `http://localhost:8080/process/${transform}/${coords}`;

    const data = {
        file: http.file(binFile, "test_image.png", "image/png")
    };

    const res = http.post(url, data);

    check(res,
        {
            "status is 200": (r) => r.status === 200
        });
}