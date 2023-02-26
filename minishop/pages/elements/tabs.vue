<template>
    <main class="main">
        <page-header title="Tabs" subtitle="Elements"></page-header>

        <div class="page-content element-tab-page">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title mb-3">Left Align Style</h2>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-tabs nav-tabs-bg"
                            id="tab-1"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content tab-content-border">
                            <div
                                class="tab-pane fade show active"
                                id="tab-1-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-1-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-1-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-1-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-tabs"
                            id="tab-2"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content tab-content-border">
                            <div
                                class="tab-pane fade show active"
                                id="tab-2-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-2-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-2-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-2-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-5 mb-4" />

                <div class="row">
                    <div class="col-12">
                        <h2 class="title mb-3">Centered Align Style</h2>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-tabs nav-tabs-bg justify-content-center"
                            id="tab-3"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content tab-content-border">
                            <div
                                class="tab-pane fade show active"
                                id="tab-3-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-3-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-3-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-3-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-tabs justify-content-center"
                            id="tab-4"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content tab-content-border">
                            <div
                                class="tab-pane fade show active"
                                id="tab-4-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-4-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-4-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-4-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-5 mb-4" />

                <div class="row">
                    <div class="col-12">
                        <h2 class="title mb-3">Line Style Tabs</h2>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-pills"
                            id="tab-5"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content pl-3 pr-3">
                            <div
                                class="tab-pane fade show active"
                                id="tab-5-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-5-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-5-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-5-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <tabs
                            class="nav-pills justify-content-center"
                            id="tab-6"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content pl-3 pr-3">
                            <div
                                class="tab-pane fade show active"
                                id="tab-6-tab1"
                            >
                                <p>
                                    Lorem ipsum dolor sit amet, consectetuer
                                    adipiscing elit. Donec odio. Quisque
                                    volutpat mattis eros. Nullam malesuada erat
                                    ut turpis. Suspendisse urna nibh, viverra
                                    non, semper suscipit, posuere a, pede. Donec
                                    nec justo eget felis facilisis fermentum.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-6-tab2">
                                <p>
                                    Nobis perspiciatis natus cum, sint dolore
                                    earum rerum tempora aspernatur numquam velit
                                    tempore omnis, delectus repellat facere
                                    voluptatibus nemo non fugiat consequatur
                                    repellendus! Enim, commodi, veniam ipsa
                                    voluptates quis amet.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-6-tab3">
                                <p>
                                    Perspiciatis quis nobis, adipisci quae
                                    aspernatur, nulla suscipit eum. Dolorum,
                                    earum. Consectetur pariatur repellat
                                    distinctio atque alias excepturi aspernatur
                                    nisi accusamus sed molestias ipsa numquam
                                    eius, iusto, aliquid, quis aut.
                                </p>
                            </div>

                            <div class="tab-pane fade" id="tab-6-tab4">
                                <p>
                                    Quis nobis, adipisci quae aspernatur, nulla
                                    suscipit eum. Dolorum, earum. Consectetur
                                    pariatur repellat distinctio atque alias
                                    excepturi aspernatur nisi accusamus sed
                                    molestias ipsa numquam eius, iusto, aliquid,
                                    quis aut.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-4 mb-4" />

                <div class="row">
                    <div class="col-12">
                        <h2 class="title mb-3">Vertical Style</h2>
                    </div>

                    <div class="col-md-6">
                        <div class="tabs-vertical">
                            <tabs
                                class="nav-tabs nav-tabs-bg flex-column"
                                id="tab-7"
                                :data="tabsData"
                            ></tabs>
                            <div class="tab-content tab-content-border">
                                <div
                                    class="tab-pane fade show active"
                                    id="tab-7-tab1"
                                >
                                    <p>
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum int dolore earum
                                        rerum tempora aspernatur numquam velit.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-7-tab2">
                                    <p>
                                        Nobis perspiciatis natus cum, sint
                                        dolore earum rerum tempora aspernatur
                                        numquam velit tempore omnis, delectus
                                        repellat facere voluptatibus nemo non
                                        fugiat consequatur repellendus! Enim,
                                        commodi, veniam ipsa voluptates quis
                                        amet.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-7-tab3">
                                    <p>
                                        Perspiciatis quis nobis, adipisci quae
                                        aspernatur, nulla suscipit eum. Dolorum,
                                        earum. Consectetur pariatur repellat
                                        distinctio atque alias excepturi
                                        aspernatur nisi accusamus sed molestias
                                        ipsa numquam eius, iusto, aliquid, quis
                                        aut.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-7-tab4">
                                    <p>
                                        Quis nobis, adipisci quae aspernatur,
                                        nulla suscipit eum. Dolorum, earum.
                                        Consectetur pariatur repellat distinctio
                                        atque alias excepturi aspernatur nisi
                                        accusamus sed molestias ipsa numquam
                                        eius, iusto, aliquid, quis aut.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="tabs-vertical">
                            <tabs
                                class="nav-tabs flex-column"
                                id="tab-8"
                                :data="tabsData"
                            ></tabs>
                            <div class="tab-content tab-content-border">
                                <div
                                    class="tab-pane fade show active"
                                    id="tab-8-tab1"
                                >
                                    <p>
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum int dolore earum
                                        rerum tempora aspernatur numquam velit.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-8-tab2">
                                    <p>
                                        Nobis perspiciatis natus cum, sint
                                        dolore earum rerum tempora aspernatur
                                        numquam velit tempore omnis, delectus
                                        repellat facere voluptatibus nemo non
                                        fugiat consequatur repellendus! Enim,
                                        commodi, veniam ipsa voluptates quis
                                        amet.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-8-tab3">
                                    <p>
                                        Perspiciatis quis nobis, adipisci quae
                                        aspernatur, nulla suscipit eum. Dolorum,
                                        earum. Consectetur pariatur repellat
                                        distinctio atque alias excepturi
                                        aspernatur nisi accusamus sed molestias
                                        ipsa numquam eius, iusto, aliquid, quis
                                        aut.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab-8-tab4">
                                    <p>
                                        Quis nobis, adipisci quae aspernatur,
                                        nulla suscipit eum. Dolorum, earum.
                                        Consectetur pariatur repellat distinctio
                                        atque alias excepturi aspernatur nisi
                                        accusamus sed molestias ipsa numquam
                                        eius, iusto, aliquid, quis aut.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import PageHeader from '~/components/elements/PageHeader';
import Tabs from '~/components/elements/Tabs';
import ElementList from '~/components/partial/elements/ElementList';

export default {
    components: {
        Tabs,
        PageHeader,
        ElementList
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'tab1',
                    title: 'Tab 1',
                    active: true
                },
                {
                    id: 'tab2',
                    title: 'Tab 2'
                },
                {
                    id: 'tab3',
                    title: 'Tab 3'
                },
                {
                    id: 'tab4',
                    title: 'Tab 4'
                }
            ]
        };
    }
};
</script>
